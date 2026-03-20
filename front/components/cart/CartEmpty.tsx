import Link from "next/link";
export default function CartLink() {
  return (
    <div className="flex flex-col items-center justify-center gap-6 rounded-lg border border-dashed border-gray-300 bg-gray-50 p-10 mt-8 shadow-sm">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        className="h-16 w-16 text-gray-400 mb-2"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        aria-hidden="true"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={1.5}
          d="M6 6h15l-1.5 9a2 2 0 01-2 1.5H8.5a2 2 0 01-2-1.5l-1.5-9z"
        />
        <circle cx="9" cy="21" r="1.5" />
        <circle cx="19" cy="21" r="1.5" />
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={1.5}
          d="M6 6V5a2 2 0 012-2h2"
        />
      </svg>
      <p className="text-lg font-semibold text-gray-600">Your cart is empty</p>
      <p className="text-sm text-gray-500">
        Looks like you haven&apos;t added anything to your cart yet!
      </p>
      <Link
        href="/product"
        className="inline-flex items-center gap-2 rounded-md bg-blue-600 px-5 py-2.5 text-sm font-medium text-white shadow transition-transform duration-150 hover:bg-blue-700 hover:scale-105"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-4 w-4 mr-1"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M3 12h18m-7-7l7 7-7 7"
          />
        </svg>
        Browse products
      </Link>
    </div>
  );
}
