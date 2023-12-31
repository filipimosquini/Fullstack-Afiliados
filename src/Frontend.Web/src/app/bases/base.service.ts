import { HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { throwError } from "rxjs";
import { LocalStorageUtils } from "../utils/localstorage";
import { environment } from "environments/environment";
export abstract class BaseService {

    public localStorage = new LocalStorageUtils();
    protected urlApi: string = environment.urlApi;

    protected GetObjectParams(filtro: any) {
        let params = new HttpParams();
        for (const key in filtro) {
            if (filtro.hasOwnProperty(key)) {
                if (filtro[key]) {
                    params = params.set(key, filtro[key]);
                }
            }
        }

        return params;
    }

    protected getJsonHeader() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected getJsonAuthHeader() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.localStorage.getUserToken()}`
            })
        };
    }

    protected getResponseData(response: any) {
        return response.data || {};
    }

    protected getResponseError(response: Response | any) {
        let customError: string[] = [];
        let customResponse = { error: { errors: [] } }

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("An unknown error has occurred");
                response.error.errors = customError;
            }
        }
        if (response.status === 500) {
            customError.push("An error has occurred,try again later or contact our support.");

            customResponse.error.errors = customError;
            return throwError(() => customResponse);
        }

        console.error(response);
        return throwError(() => response);
    }
}
