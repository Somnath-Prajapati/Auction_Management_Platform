import { environment } from "./enviroment";


const BASE_URL = environment.apiUrl;

export const ApiEndpoints = {
  USER: `${BASE_URL}/User`,
  COUNTRY: `${BASE_URL}/Country`
};
