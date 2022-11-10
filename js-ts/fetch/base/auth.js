import Cookies from "js-cookie";
import { tokenCookieName } from "../../fetch/config";

class Auth {
    getToken() {
        const token = Cookies.get(tokenCookieName);
        if (token) {
            return token;
        }
        return null;
    }

    setToken(token) {
        Cookies.set(tokenCookieName, token, { sameSite: 'strict', secure: true, expires: 1 });
    }
}

export default Auth;