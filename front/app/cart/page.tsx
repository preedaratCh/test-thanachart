import PageHeader from "@/components/PageHeader";
import CartContent from "@/components/cart/CartContent";

export default function CartPage() {
  return (
    <div>
      <PageHeader title="Cart" subtitle="Your session cart items" />
      <CartContent />
    </div>
  );
}
