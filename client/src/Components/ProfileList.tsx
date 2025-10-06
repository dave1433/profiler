import React from 'react'
import type { ProfileDto } from '../generated-ts-client.ts'

interface ProfileListProps {
    profiles: ProfileDto[];
    selectedProfile: ProfileDto | null;
    onSelectProfile: (profile: ProfileDto) => void;
}

export function ProfileList({ profiles, selectedProfile, onSelectProfile }: ProfileListProps) {
    return (
        <div className="profile-list">
            {profiles.map(p => (
                <div key={p.id}
                     className={`profile-card${selectedProfile?.id === p.id ? " selected" : ""}`}
                     onClick={() => onSelectProfile(p)}>
                    {p.photoUrl && <img src={p.photoUrl} alt="Profile" className="profile-img" />}
                    <h3 className="profile-name">{p.firstname} {p.lastname}</h3>
                    <p>Age: {p.age}</p>
                    <p>Occupation: {p.occupation}</p>
                    <p>City: {p.city}</p>
                </div>
            ))}
        </div>
    )
}