import { FluentProvider } from "@fluentui/react-components";
import { webDarkTheme } from "@fluentui/react-components";
import { webLightTheme } from "@fluentui/react-components";
import React from "react";
import { createRoot } from "react-dom/client";
import App from "./App";
import { ComposableProvider } from "./utils/ComposableProvider";

let theme = window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches ? webDarkTheme : webLightTheme;
window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", (event) => {
    theme = event.matches ? webDarkTheme : webLightTheme;
    render();
});

const rootElement = document.getElementById("root")!;
const root = createRoot(rootElement);

function render() {
    const providerItems = [{ provider: FluentProvider, props: { theme: theme } }];
    // root.render(
    //     <FluentProvider theme={theme}>
    //         <App />
    //     </FluentProvider>
    // );

    root.render(
        <React.StrictMode>
            <ComposableProvider providerItems={providerItems}>
                <App />
            </ComposableProvider>
        </React.StrictMode>
    );
}

render();
