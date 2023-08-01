import { JSXElementConstructor, PropsWithChildren } from "react";

export type ComposableProviderItem = {
    provider: JSXElementConstructor<PropsWithChildren<unknown>>;
    props: {};
};

export type ComposableProviderProps = PropsWithChildren & {
    providerItems: ReadonlyArray<ComposableProviderItem>;
};

export const ComposableProvider = ({ providerItems, children }: ComposableProviderProps) => {
    return (
        <>
            {providerItems.reduceRight((acc, Item) => {
                return <Item.provider {...Item.props}>{acc}</Item.provider>;
            }, children)}
        </>
    );
};
