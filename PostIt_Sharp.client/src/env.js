export const dev = window.location.origin.includes('localhost')
// NOTE make sure to change your base URL to the correct port when working with C#
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false
export const domain = 'dev-ynre1pz7.us.auth0.com'
export const clientId = 'koI64hAUtt7sCgyAXvri3QAIUMhBENg4'
export const audience = 'https://sandbox.com'
