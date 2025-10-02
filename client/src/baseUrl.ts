import {ProfilerClient} from "./generated-ts-client.ts";

const isProduction = import.meta.env.PROD;

const prod= "https://server-profiler-5099.fly.dev"
const dev = "http://localhost:5261"

export const finalUrl = isProduction ? prod : dev;
export const profilerClient = new ProfilerClient(finalUrl);