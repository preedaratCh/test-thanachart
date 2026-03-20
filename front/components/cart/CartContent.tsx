"use client";

import CartEmpty from "@/components/cart/CartEmpty";
import CartGrid from "@/components/cart/CartGrid";
import { useCart } from "@/components/cart/CartProvider";

export default function CartContent() {
  const { items } = useCart();

  if (items.length === 0) {
    return <CartEmpty />;
  }

  return <CartGrid />;
}
