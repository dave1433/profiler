const isProduction = import.meta.env.PROD;

const prod= "https://server-profiler-5099.fly.dev"
const dev = "http://localhost:5228"

export const finalUrl = isProduction ? prod : dev;