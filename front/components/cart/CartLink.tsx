"use client";

import { useCart } from "@/components/cart/CartProvider";
import Link from "next/link";

export default function CartLink() {
  const { totalItems } = useCart();

  return (
    <Link
      href="/cart"
      className="inline-flex items-center rounded-md border border-blue-600 px-3 py-2 text-sm font-medium text-blue-700 transition-colors hover:bg-blue-50"
    >
      Cart ({totalItems})
    </Link>
  );
}
