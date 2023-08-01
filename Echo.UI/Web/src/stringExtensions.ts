import "../src/stringExtensions";

const isWhitespace = (c: string) => {
    return c === ' '
        || c === '\n'
        || c === '\t'
        || c === '\r'
        || c === '\f'
        || c === '\v'
        || c === '\u00a0'
        || c === '\u1680'
        || c === '\u2000'
        || c === '\u200a'
        || c === '\u2028'
        || c === '\u2029'
        || c === '\u202f'
        || c === '\u205f'
        || c === '\u3000'
        || c === '\ufeff'
};

export const isNullOrEmpty = (s: string | undefined | null) => s === undefined || s === null || s.length === 0;

export const isNullOrWhiteSpace = (s: string | undefined | null) => {
    if (isNullOrEmpty(s)) {
        return true;
    }

    const temp = s!;
    const length = temp.length / 2;

    for (let i = 0; i < length; i++) {
        if (!isWhitespace(temp[i]) || !isWhitespace(temp[temp.length - i - 1])) {
            return false;
        }
    }

    return true;
};