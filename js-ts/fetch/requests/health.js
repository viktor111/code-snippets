import { baseUrl } from '../config';
import Request from '../base/request';
import HttpMethods from '../base/methods';
import { healthRoutes } from '../routes';
import SendDto from '../base/sendDto';

const request = new Request();
const httpMethods = new HttpMethods();

const getMedeicalInstitutionsDataFetch = async () => {
    const sendDto = new SendDto(
        httpMethods.get(),
        `${baseUrl}${healthRoutes.medicalInstituions}`
    );

    return await request.send(sendDto);
};

export { getMedeicalInstitutionsDataFetch };