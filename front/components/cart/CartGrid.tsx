"use client";

import { useCart } from "@/components/cart/CartProvider";
import Image from "next/image";

export default function CartGrid() {
  const { items, subtotal, updateQuantity, removeFromCart, clearCart, checkout } = useCart();

  const handleUpdateQuantity = async (productId: string, quantity: number) => {
    const result = await updateQuantity(productId, quantity);
    if (!result.ok && result.message) {
      window.alert(result.message);
    }
  };

  const handleCheckout = async () => {
    const result = await checkout();
    if (!result.ok && result.message) {
      window.alert(result.message);
      return;
    }
    if (result.ok && result.message) {
      window.alert(result.message);
    }
  };
  return (
    <div className="space-y-4 p-6">
          <ul className="space-y-3">
            {items.map((item) => (
              <li
                key={item.product.id}
                className="flex flex-col gap-3 rounded-lg border p-4 sm:flex-row sm:items-center sm:justify-between"
              >
                <div className="flex items-center gap-3">
                  {item.product.imageUrl ? (
                    <Image
                      src={item.product.imageUrl}
                      alt={item.product.name}
                      width={72}
                      height={72}
                      className="h-[72px] w-[72px] rounded-md object-cover"
                    />
                  ) : (
                    <div className="flex h-[72px] w-[72px] items-center justify-center rounded-md bg-gray-200 text-xs text-gray-500">
                      No image
                    </div>
                  )}
                  <div>
                    <p className="font-medium text-gray-900">{item.product.name}</p>
                    <p className="text-sm text-gray-600">
                      {item.product.price.toLocaleString("en-US")} THB
                    </p>
                  </div>
                </div>

                <div className="flex items-center gap-2">
                  <button
                    type="button"
                    onClick={() =>
                      void handleUpdateQuantity(
                        item.product.id,
                        Math.max(item.quantity - 1, 0)
                      )
                    }
                    className="rounded-md border px-3 py-1 text-sm hover:bg-gray-50"
                  >
                    -
                  </button>
                  <span className="min-w-8 text-center">{item.quantity}</span>
                  <button
                    type="button"
                    onClick={() =>
                      void handleUpdateQuantity(item.product.id, item.quantity + 1)
                    }
                    className="rounded-md border px-3 py-1 text-sm hover:bg-gray-50"
                  >
                    +
                  </button>
                  <button
                    type="button"
                    onClick={() => removeFromCart(item.product.id)}
                    className="ml-2 rounded-md border border-red-200 px-3 py-1 text-sm text-red-600 hover:bg-red-50"
                  >
                    Remove
                  </button>
                </div>
              </li>
            ))}
          </ul>

          <div className="flex flex-col gap-3 rounded-lg border p-4 sm:flex-row sm:items-center sm:justify-between">
            <p className="text-lg font-semibold text-gray-900">
              Subtotal: {subtotal.toLocaleString("en-US")} THB
            </p>
            <div className="flex items-center gap-2">
                <button
              type="button"
              onClick={clearCart}
              className="rounded-md border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
            >
              Clear cart
            </button>
            <button
              type="button"
              onClick={() => void handleCheckout()}
              className="rounded-md border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
            >
              Checkout
            </button>
            </div>
            
          </div>
        </div>
  );
}