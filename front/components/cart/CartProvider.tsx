"use client";

import { CartItem } from "@/types/cart";
import { InventoryService } from "@/services/inventory.service";
import { OrderService } from "@/services/order.service";
import { Product } from "@/types/product";
import { createContext, useContext, useMemo, useSyncExternalStore } from "react";

type CartActionResult = {
  ok: boolean;
  message?: string;
};

type CartContextValue = {
  items: CartItem[];
  addToCart: (product: Product) => Promise<CartActionResult>;
  removeFromCart: (productId: string) => void;
  updateQuantity: (
    productId: string,
    quantity: number
  ) => Promise<CartActionResult>;
  checkout: () => Promise<CartActionResult>;
  clearCart: () => void;
  totalItems: number;
  subtotal: number;
};

const CART_SESSION_KEY = "cart_session_v1";
const EMPTY_CART: CartItem[] = [];
const listeners = new Set<() => void>();
let cartItemsStore: CartItem[] = [];

function readSessionCart(): CartItem[] {
  if (typeof window === "undefined") {
    return [];
  }

  const raw = window.sessionStorage.getItem(CART_SESSION_KEY);
  if (!raw) {
    return [];
  }

  try {
    return JSON.parse(raw) as CartItem[];
  } catch {
    window.sessionStorage.removeItem(CART_SESSION_KEY);
    return [];
  }
}

if (typeof window !== "undefined") {
  cartItemsStore = readSessionCart();
}

function emitCartChange() {
  listeners.forEach((listener) => listener());
}

function subscribe(listener: () => void) {
  listeners.add(listener);
  return () => {
    listeners.delete(listener);
  };
}

function getSnapshot() {
  return cartItemsStore;
}

function getServerSnapshot() {
  return EMPTY_CART;
}

function setCartItems(nextItems: CartItem[]) {
  cartItemsStore = nextItems;
  if (typeof window !== "undefined") {
    window.sessionStorage.setItem(CART_SESSION_KEY, JSON.stringify(nextItems));
  }
  emitCartChange();
}

const CartContext = createContext<CartContextValue | null>(null);

export function CartProvider({ children }: { children: React.ReactNode }) {
  const items = useSyncExternalStore(subscribe, getSnapshot, getServerSnapshot);

  const value = useMemo<CartContextValue>(() => {
    const inventoryService = new InventoryService();

    const validateInventory = async (
      productId: string,
      targetQuantity: number
    ): Promise<CartActionResult> => {
      try {
        const availableQuantity = await inventoryService.getAvailableQuantity(
          productId
        );

        if (availableQuantity === null) {
          return { ok: false, message: "Product inventory not found." };
        }

        if (targetQuantity > availableQuantity) {
          return {
            ok: false,
            message: `Not enough stock. Available: ${availableQuantity}`,
          };
        }

        return { ok: true };
      } catch {
        return {
          ok: false,
          message: "Unable to check inventory right now.",
        };
      }
    };

    const addToCart = async (product: Product): Promise<CartActionResult> => {
      const currentItems = getSnapshot();
      const existing = currentItems.find((item) => item.product.id === product.id);
      const nextQuantity = existing ? existing.quantity + 1 : 1;

      const inventoryCheck = await validateInventory(product.id, nextQuantity);
      if (!inventoryCheck.ok) {
        return inventoryCheck;
      }

      const latestItems = getSnapshot();
      const latestExisting = latestItems.find(
        (item) => item.product.id === product.id
      );

      if (!latestExisting) {
        setCartItems([...latestItems, { product, quantity: 1 }]);
      } else {
        setCartItems(
          latestItems.map((item) =>
            item.product.id === product.id
              ? { ...item, quantity: item.quantity + 1 }
              : item
          )
        );
      }

      return { ok: true };
    };

    const removeFromCart = (productId: string) => {
      const latestItems = getSnapshot();
      setCartItems(latestItems.filter((item) => item.product.id !== productId));
    };

    const updateQuantity = async (
      productId: string,
      quantity: number
    ): Promise<CartActionResult> => {
      if (quantity <= 0) {
        removeFromCart(productId);
        return { ok: true };
      }

      const inventoryCheck = await validateInventory(productId, quantity);
      if (!inventoryCheck.ok) {
        return inventoryCheck;
      }

      const latestItems = getSnapshot();
      setCartItems(
        latestItems.map((item) =>
          item.product.id === productId ? { ...item, quantity } : item
        )
      );

      return { ok: true };
    };

    const clearCart = () => {
      setCartItems([]);
    };

    const checkout = async (): Promise<CartActionResult> => {
      const currentItems = getSnapshot();
      const orderService = new OrderService();
      const order = await orderService.createOrder(currentItems);
      if (!order.ok) {
        return order;
      }
      clearCart();
      return { ok: true, message: "Checkout success." };
    };

    const totalItems = items.reduce((sum, item) => sum + item.quantity, 0);
    const subtotal = items.reduce(
      (sum, item) => sum + item.product.price * item.quantity,
      0
    );

    return {
      items,
      addToCart,
      removeFromCart,
      updateQuantity,
      clearCart,
      checkout,
      totalItems,
      subtotal,
    };
  }, [items]);

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
}

export function useCart() {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error("useCart must be used within CartProvider");
  }
  return context;
}
