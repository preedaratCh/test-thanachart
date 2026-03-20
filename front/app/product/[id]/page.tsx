import React from 'react';

interface ProductPageProps {
    params: {
        id: string;
    };
}

const ProductPage: React.FC<ProductPageProps> = ({ params }) => {
    return (
        <main style={{ padding: '2rem' }}>
            <header>
                <h1>Product Details</h1>
            </header>
            <section>
                <p>
                    <strong>Product ID:</strong> {params.id}
                </p>
            </section>
        </main>
    );
};

export default ProductPage;