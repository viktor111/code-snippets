import Cookies from "js-cookie";
import { getProfile } from "./requests/user";

export async function refreshDataFromServer(setUserDATA, setUserRole) {
    const token = Cookies.get('__Host-token');

    if (token) {

        let responseUserData = await getProfile();
        let fetchResponse = responseUserData.status; // <-- RESULT!!!

        if (fetchResponse === 'ok') {

            Cookies.set('__Host-token', token, { sameSite: 'strict', secure: true, expires: 1 });

            if (setUserDATA && setUserRole) {

                setUserDATA(responseUserData.data); //<-- DATA FOR TESTING!!!

                setUserRole(responseUserData.data.ProfileRole);
            }

        } else {
            Cookies.set('__Host-token', token, { sameSite: 'strict', secure: true, expires: 0 });

            if (setUserRole) {
                setUserRole(false);
            }
        }

    } else {

        if (setUserRole) {
            setUserRole(false);
        }
    };
};