import {useState} from "react";
import {profilerClient} from "../baseUrl.ts";
import type {CreateProfileDto, ProfileDto, UpdateProfileDto} from "../generated-ts-client.ts";

export function useProfileHandlers(profiles: ProfileDto[], setProfiles: (profiles: ProfileDto[]) => void) {
    const [selectedProfile, setSelectedProfile] = useState<ProfileDto | null>(null)
    const [myForm, setMyForm] = useState<CreateProfileDto>({
        age: 0,
        firstname: "",
        lastname: "",
        occupation: "",
        city: "",
        photoUrl: ""
    })

    const [showForm, setShowForm] = useState(false)
    const [isEditing, setIsEditing] = useState(false)

    const handleSelectProfile = (profile: ProfileDto) => {
        setSelectedProfile(profile)
        setMyForm({ ...profile })
        setShowForm(true)
        setIsEditing(true)
    }

    const handleAddNew = () => {
        setSelectedProfile(null)
        setMyForm({
            age: 0,
            firstname: "",
            lastname: "",
            occupation: "",
            city: "",
            photoUrl: ""
        })
        setShowForm(true)
        setIsEditing(false)
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const { firstname, lastname, age, occupation, city } = myForm;
        if (!firstname || !lastname || !age || !occupation || !city) {
            alert("Please fill in all fields.");
            return;
        }
        if (isEditing && selectedProfile) {
            const updateDto = {
                id: selectedProfile.id,
                age: myForm.age,
                firstname: myForm.firstname,
                lastname: myForm.lastname,
                occupation: myForm.occupation,
                city: myForm.city,
                photoUrl: myForm.photoUrl
            } as UpdateProfileDto;

            profilerClient.updateProfile(updateDto).then(updated => {
                setProfiles(profiles.map(p => p.id === updated.id ? updated : p));
                setShowForm(false);
                setSelectedProfile(null);
            }).catch(err => {
                alert("Profile update failed.");
                console.error(err);
            });
        } else {
            profilerClient.createProfile(myForm).then(newProfile => {
                setProfiles([...profiles, newProfile]);
                setShowForm(false);
                setSelectedProfile(null);
            }).catch(err => {
                alert("Profile creation failed.");
                console.error(err);
            });
        }
    }

    const handleDelete = () => {
        if(selectedProfile) {
            profilerClient.deleteProfile({ id: selectedProfile.id }).then(result => {
                setProfiles(profiles.filter(p => p.id !== result.id));
                setSelectedProfile(null);
                setShowForm(false);
            });
        }
    }
    return {
        selectedProfile, setSelectedProfile,
        myForm, setMyForm,
        showForm, setShowForm,
        isEditing, setIsEditing,
        handleSelectProfile,
        handleAddNew,
        handleSubmit,
        handleDelete,
    }
}