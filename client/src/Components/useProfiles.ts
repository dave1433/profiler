import {useEffect, useState} from "react";
import type {ProfileDto} from "../generated-ts-client.ts";
import {profilerClient} from "../baseUrl.ts";

export function useProfiles() {
    const [profiles, setProfiles] = useState<ProfileDto[]>([])
    useEffect(() => {
        profilerClient.getAllProfiles().then(r => setProfiles(r))
    }, [])
    return {profiles, setProfiles}
}