"use client";

import { useCart } from "@/components/cart/CartProvider";
import { Product } from "@/types/product";
import Image from "next/image";

type ProductGridProps = {
  products: Product[];
};

export default function ProductGrid({ products }: ProductGridProps) {
  const { addToCart } = useCart();
  const handleAddToCart = async (product: Product) => {
    const result = await addToCart(product);
    if (!result.ok && result.message) {
      window.alert(result.message);
    }
  };

  return (
    <ul className="grid grid-cols-1 gap-4 p-6 sm:grid-cols-2 lg:grid-cols-4">
      {products.map((product) => (
        <li
          key={product.id}
          className="flex h-full flex-col overflow-hidden rounded-lg border bg-white shadow-sm"
        >
          {product.imageUrl ? (
            <Image
              src={product.imageUrl}
              alt={product.name}
              width={600}
              height={400}
              sizes="(max-width: 640px) 100vw, (max-width: 1024px) 50vw, 33vw"
              className="h-48 w-full object-cover"
            />
          ) : (
            <div className="flex h-48 w-full items-center justify-center bg-gray-200 text-sm text-gray-500">
              No image
            </div>
          )}
          <div className="flex flex-1 flex-col p-4">
            <strong className="block line-clamp-2 text-lg text-gray-900">
              {product.name}
            </strong>
            <p className="mt-2 line-clamp-2 text-sm text-gray-600">
              {product.description}
            </p>
            <div className="mt-auto pt-4">
              <p className="font-semibold text-blue-700">
                {product.price.toLocaleString("en-US")} THB
              </p>
              <button
                type="button"
                onClick={() => void handleAddToCart(product)}
                className="mt-2 w-full rounded-md bg-blue-600 px-3 py-2 text-sm font-medium text-white transition-colors hover:bg-blue-700"
              >
                Add to cart
              </button>
            </div>
          </div>
        </li>
      ))}
    </ul>
  );
}
