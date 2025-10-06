import './App.css'
import { useProfileHandlers } from './Components/UseProfileHandlers.ts';
import { useProfiles } from './Components/useProfiles.ts';
import { ProfileForm } from './ProfileForm.tsx';
import { ProfileList } from './Components/ProfileList.tsx';

function App() {
    const { profiles, setProfiles } = useProfiles();

    const {
        myForm,
        setMyForm,
        showForm,
        setShowForm,
        isEditing,
        setIsEditing,
        selectedProfile,
        setSelectedProfile,
        handleAddNew,
        handleSubmit,
        handleDelete,
        handleSelectProfile
    } = useProfileHandlers(profiles, setProfiles)

    return (
        <div className="app-container">
            <div className="header">
                <h1 className="title">ProfilerX</h1>
                <p className="subtitle">Discover and connect with people in your city</p>
            </div>
            <button onClick={handleAddNew} className="add-btn">
                {isEditing ? "Edit Profile" : "Add New Profile"}
            </button>
            {showForm && (
                <>
                    {isEditing && (
                        <button className="delete-btn" onClick={handleDelete}>
                            Delete Profile
                        </button>
                    )}
                    <ProfileForm
                        myForm={myForm}
                        setMyForm={setMyForm}
                        onSubmit={handleSubmit}
                        isEditing={isEditing}
                    />
                    <button onClick={() => { setShowForm(false); setSelectedProfile(null); }}>
                        Cancel
                    </button>
                </>
            )}
            <hr />
            <h2 className="h2">Profiles</h2>
            <ProfileList
                profiles={profiles}
                selectedProfile={selectedProfile}
                onSelectProfile={handleSelectProfile}
            />
        </div>
    );
}

export default App;