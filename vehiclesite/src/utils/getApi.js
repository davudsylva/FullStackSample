export function getApi(config) {
    //  Based upon the environment, wouk out the appropriate API to use
    return `https://localhost:44307/`;
  }
  
  export function getContactApi(config) {
    return getApi(config) + "/site/contactRequest"
  }