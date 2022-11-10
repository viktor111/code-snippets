import { baseUrl } from '../config';
import Request from '../base/request';
import HttpMethods from '../base/methods';
import { userRoutes } from '../routes';
import SendDto from '../base/sendDto';

const request = new Request();
const httpMethods = new HttpMethods();

const getProfile = async () => {
    const sendDto = new SendDto(
        httpMethods.get(),
        `${baseUrl}${userRoutes.loginData}`
    );

    return await request.send(sendDto);
};

const loginToken = async (egn, password) => {
    const sendDto = new SendDto(
        httpMethods.post(),
        `${baseUrl}${userRoutes.loginToken}`,
        { egn, password }
    );

    return await request.send(sendDto);
};


const changeEmail = async (currentEmail, newEmail) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.changeEmail}`,
        { current: currentEmail, new: newEmail }
    );

    return await request.send(sendDto);
}

const changePassword = async (currentPassword, newPassword, confirmPassword) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.changePassword}`,
        { current:currentPassword, new:newPassword, confirmNew:confirmPassword }
    );

    return await request.send(sendDto);
}

const setBirthDate = async (birthDate) => {
    const sendDto = new SendDto(
        httpMethods.post(),
        `${baseUrl}${userRoutes.setBirthDate}`,
        { birthDate }
    );

    return await request.send(sendDto);
};

const sendEmail = async (email) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.setEmail}`,
        { email }
    );

    return await request.send(sendDto);
}

const confirmEmailCode = async (code) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.confirmCode}`,
        { code }
    );

    return await request.send(sendDto);
}

const resetPassword = async (email) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.resetPassword}`,
        { email }
    );

    return await request.send(sendDto);
}

const sendBirthDateServer = async (birthDate) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.sendBirthDate}`,
        { birthDate }
    );

    return await request.send(sendDto);
};

const changePhoneNumber = async (phoneNumber) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${userRoutes.changePhoneNumber}`,
        { phoneNumber }
    );

    return await request.send(sendDto);
}

export {
    getProfile,
    loginToken,
    changeEmail,
    changePassword,
    setBirthDate,
    sendEmail,
    confirmEmailCode,
    resetPassword,
    sendBirthDateServer,
    changePhoneNumber
};