import { baseUrl } from '../config';
import Request from '../base/request';
import HttpMethods from '../base/methods';
import { claimRoutes, userRoutes } from '../routes';
import SendDto from '../base/sendDto';

const request = new Request();
const httpMethods = new HttpMethods();

const uploadDocument = async (file) => {
    const sendDto = new SendDto(
        httpMethods.post(),
        `${baseUrl}${claimRoutes.uploadDocument}`,
        file, true
    );

    return await request.send(sendDto);
};

const createClaim = async (claim) => {

    claim.ClaimInformation.EGN = claim.ClaimInformation.EGN.replace(/\s/g, '');

    const sendDto = new SendDto(
        httpMethods.post(),
        `${baseUrl}${claimRoutes.createClaim}`,
        claim
    );

    return await request.send(sendDto);
};

const updateClaim = async (claim) => {
    const sendDto = new SendDto(
        httpMethods.patch(),
        `${baseUrl}${claimRoutes.updateClaim}`,
        claim
    );

    return await request.send(sendDto);
};

export {
    uploadDocument,
    createClaim,
    updateClaim
};