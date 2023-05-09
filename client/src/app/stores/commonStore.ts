import React, { useEffect } from "react";
import { autorun, makeAutoObservable, reaction } from 'mobx';
import { ServerError } from "../models/serverError";
import { setToken } from "../api/authorization";

export default class CommonStore {
      error: ServerError | null = null;
      accessToken: string | null = localStorage.getItem("jwt");
      appLoaded = false;

      constructor() {
            makeAutoObservable(this);


            reaction( // it runs only when token or list change, it is called mobx auto run
                  () => this.accessToken,
                  accessToken => {
                        if (accessToken) {
                              localStorage.setItem("jwt", accessToken);
                              setToken(accessToken);
                        } else {
                              localStorage.removeItem("jwt");
                        }
                  }
            )
      }

      apiSetToken = () => {
            setToken(this.accessToken);
      }

      setServerError = (error: ServerError) => {
            this.error = error;

      }

      setToken = async (token: string | null) => {
            this.accessToken = token;
      }

      setAppLoaded = () => {
            this.appLoaded = true;
      }
}