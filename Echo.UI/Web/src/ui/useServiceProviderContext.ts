import { createContext, useContext } from "react";
import { IServiceProvider, ServiceManager } from "../core";
import { IAccountService, IContactService, ISubscriptionService, UserServiceKey } from "../core/user";
import { NativeAccountService, NativeContactService, NativeSubscriptionService } from "../core/user/native";

const serviceProvider: IServiceProvider =
    new ServiceManager()
        .register<IAccountService>(
            UserServiceKey.AccountService,
            new NativeAccountService())
        .register<IContactService>(
            UserServiceKey.ContactService,
            new NativeContactService())
        .register<ISubscriptionService>(
            UserServiceKey.SubscriptionService,
            new NativeSubscriptionService());

const ServiceProviderContext = createContext(serviceProvider);

export const useServiceProviderContext = () => useContext(ServiceProviderContext);