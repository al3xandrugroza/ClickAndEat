const PROXY_CONFIG = [
  {
    context: [
      "/.well-known",
    ],
    target: 'https://localhost:7255',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
