import Status from "./status";
import OptionsGenerator from "./optionsGenerator";
import ServerResponse from "./serverResponse";

class Request {
    #statusTypes;
    #optionsGenerator;

    constructor() {
        this.#statusTypes = new Status();
        this.#optionsGenerator = new OptionsGenerator();
    }

    async send(sendDto) {
        try {
            this.#validate(sendDto.method, sendDto.path);
            const options = this.#optionsGenerator.generate(sendDto.method, sendDto.body, sendDto.isFormData);

            const response = await this.#performFetch(sendDto.path, options);
            console.log(response);
            const data = await this.#getJsonData(response);
            console.log(data); 
            return data;
        }
        catch (err) {
            console.error(err);
            return new ServerResponse(this.#statusTypes.error(), null);
        }
    }

    async #performFetch(path, options) {
        const response = await fetch(path, options);
        console.log(response);
        if (response.ok === false) {
            const err = await response.json();
            throw new Error(err.message);
        }

        return response;
    }

    async #getJsonData(response) {
        const data = await response.json();
        console.info(data);
        return new ServerResponse(this.#statusTypes.ok(), data.Euroins.Values[0]);
    }

    #validate(method, path) {
        if (!method || !path) {
            throw new Error('Request must have method and path');
        }
    }
}

export default Request;