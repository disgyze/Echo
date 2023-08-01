import { useState } from "react";
import { IContactService, UserServiceKey } from "../../core/user";
import { useServiceProviderContext } from "../useServiceProviderContext";

export const useFetchContacts = () => {
    const [isLoading, setIsLoading] = useState(false);

    const serviceProvider = useServiceProviderContext();
    const contactService = serviceProvider.getService<IContactService>(UserServiceKey.ContactService);

    if (!contactService) {
        throw Error("ContactService not available");
    }

    const fetchContacts = async () => {
        setIsLoading(true);
        try {
            return await contactService.getAll();
        } finally {
            setIsLoading(false);
        }
    };

    return [fetchContacts, isLoading] as const;
};
