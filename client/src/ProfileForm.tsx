import React from 'react';
import type { CreateProfileDto } from './generated-ts-client.ts';

interface ProfileFormProps {
    myForm: CreateProfileDto;
    setMyForm: (form: CreateProfileDto) => void;
    onSubmit: (e: React.FormEvent) => void;
    isEditing: boolean;
}

export function ProfileForm({ myForm, setMyForm, onSubmit, isEditing }: ProfileFormProps) {
    return (
        <form className="form-container" onSubmit={onSubmit}>
            <h2 className="h2">{isEditing ? "Edit Profile" : "Add a New Profile"}</h2>
            <input
                className="input"
                value={myForm.photoUrl ?? ""}
                onChange={e => setMyForm({ ...myForm, photoUrl: e.target.value })}
                placeholder="Photo url!"
            />
            <input
                className="input"
                value={myForm.firstname}
                onChange={e => setMyForm({ ...myForm, firstname: e.target.value })}
                placeholder="Name!"
            />
            <input
                className="input"
                value={myForm.lastname}
                onChange={e => setMyForm({ ...myForm, lastname: e.target.value })}
                placeholder="Lastname"
            />
            <input
                className="input"
                value={myForm.age}
                onChange={e => setMyForm({ ...myForm, age: Number.parseInt(e.target.value) })}
                type="number"
                placeholder="Age"
            />
            <input
                className="input"
                value={myForm.occupation}
                onChange={e => setMyForm({ ...myForm, occupation: e.target.value })}
                placeholder="Occupation!"
            />
            <input
                className="input"
                value={myForm.city}
                onChange={e => setMyForm({ ...myForm, city: e.target.value })}
                placeholder="City!"
            />
            <button className="submit-btn" type="submit">
                {isEditing ? "Save Changes" : "Add Profile"}
            </button>
        </form>
    );
}