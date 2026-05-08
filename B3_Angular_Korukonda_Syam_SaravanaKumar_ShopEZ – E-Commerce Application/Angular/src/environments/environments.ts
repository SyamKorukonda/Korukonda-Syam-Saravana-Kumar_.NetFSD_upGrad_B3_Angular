export const environment = {
  production: false,
  // Matches GlobalConfiguration.BaseUrl in ocelot.json
  // All API calls route through Ocelot API Gateway on port 7074
  gatewayUrl: 'https://localhost:7074/gateway'
};
