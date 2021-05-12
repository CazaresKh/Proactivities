import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { User } from "../models/user";
import { UserFormValues } from "../models/userFormValues";
import { store } from "./store";

export default class UserStore {
  user: User | null = null;
  fbAccessToken: string | null = null;
  fbloading = false;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  login = async (credentials: UserFormValues) => {
    try {
      const user = await agent.Account.login(credentials);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      history.push("/activities");
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };

  logout = () => {
    store.commonStore.setToken(null);
    window.localStorage.removeItem("jwt");
    this.user = null;
    history.push("/");
  };

  getUser = async () => {
    try {
      const user = await agent.Account.current();
      runInAction(() => (this.user = user));
    } catch (error) {
      console.log(error);
    }
  };

  register = async (credentials: UserFormValues) => {
    try {
      const user = await agent.Account.register(credentials);
      store.commonStore.setToken(user.token);
      runInAction(() => (this.user = user));
      history.push("/activities");
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };

  setImage = (image: string) => {
    if (this.user) {
      this.user.image = image;
    }
  };

  setDisplayName = (name: string) => {
    if (this.user) {
      this.user.displayName = name;
    }
  };

  getFacebookLoginStatus = async () => {
    window.FB.getLoginStatus((response) => {
      if (response.status === "connected") {
        this.fbAccessToken = response.authResponse.accessToken;
      }
    });
  };

  facebookLogin = () => {
    this.fbloading = true;
    const apiLogin = (accessToken: string) => {
      agent.Account.fblogin(accessToken)
        .then((user) => {
          store.commonStore.setToken(user.token);
          runInAction(() => {
            this.user = user;
            this.fbloading = false;
          });
          history.push("/activities");
        })
        .catch((error) => {
          console.log(error);
          runInAction(() => (this.fbloading = false));
        });
    };

    if (this.fbAccessToken) {
      apiLogin(this.fbAccessToken);
    } else {
      window.FB.login(
        (response) => {
          apiLogin(response.authResponse.accessToken);
        },
        { scope: "public_profile,email" }
      );
    }
  };
}
