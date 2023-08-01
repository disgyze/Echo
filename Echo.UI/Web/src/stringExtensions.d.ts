import { isNullOrEmpty, isNullOrWhiteSpace } from "./stringExtensions";

declare global {
    interface StringConstructor {
        isNullOrEmpty(s: string | undefined): boolean;
        isNullOrWhiteSpace(s: string | undefined): boolean;
    }
}