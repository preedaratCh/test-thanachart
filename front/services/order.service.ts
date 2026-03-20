import { CartItem } from "@/types/cart";

type OrderRequest = {
  customerId: string;
  items: Array<{
    productId: string;
    quantity: number;
  }>;
};

type OrderResponse = {
  ok: boolean;
  message?: string;
};

export class OrderService {
  private readonly baseUrl: string;

  constructor(apiBaseUrl: string = "http://localhost:5000/api/orders") {
    this.baseUrl = apiBaseUrl;
  }

  async createOrder(cartItems: CartItem[]): Promise<OrderResponse> {
    if (cartItems.length === 0) {
      return { ok: false, message: "Cart is empty." };
    }

    const customerId = process.env.NEXT_PUBLIC_DEMO_CUSTOMER_ID;
    if (!customerId) {
      return {
        ok: false,
        message: "Missing NEXT_PUBLIC_DEMO_CUSTOMER_ID for checkout.",
      };
    }

    const payload: OrderRequest = {
      customerId,
      items: cartItems.map((item) => ({
        productId: item.product.id,
        quantity: item.quantity,
      })),
    };

    try {
      const response = await fetch(this.baseUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(payload),
        cache: 'no-store'
      });

      if (!response.ok) {
        let message = "Checkout failed.";
        try {
          const data = (await response.json()) as { message?: string };
          if (data.message) {
            message = data.message;
          }
        } catch {}
        return { ok: false, message };
      }

      return { ok: true };
    } catch (error: unknown) {
      return {
        ok: false,
        message: `Unable to create order: ${
          error instanceof Error ? error.message : String(error)
        }`,
      };
    }
  }
}
