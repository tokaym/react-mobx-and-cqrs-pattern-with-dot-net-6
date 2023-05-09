import axios from 'axios';

// use a middleware to intercept this action and pull token
// axios will automatically include header in all http requests

export function setToken(token: string | null) {
  axios.defaults.headers.common['Authorization'] =
      `Bearer ${token}`;
}