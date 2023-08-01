import { ConversationMessageItem, ConversationMessageProps } from "./ConversationMessageItem";

export type ConversationMessageListProps = Readonly<{
    messages: ConversationMessageProps[];
}>;

export const ConversationMessageList = (props: ConversationMessageListProps) => {
    const { messages } = props;

    return (
        <div>
            {messages.map(props => <ConversationMessageItem {...props} />)}
        </div>
    );
};