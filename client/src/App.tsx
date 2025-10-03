import './App.css'
import { useEffect, useState } from 'react'
import { profilerClient } from './baseUrl.ts'
import type { CreateProfileDto, Profiler } from './generated-ts-client.ts'

function App() {
    const [profiles, setProfiles] = useState<Profiler[]>([])
    const [myForm, setMyForm] = useState<CreateProfileDto>({
        age: 0,
        firstname: "",
        lastname: "",
        occupation: "",
        city: "",
        photoUrl: ""
    })

    useEffect(() => {
        profilerClient.getAllProfiles().then(r => setProfiles(r))
    }, [])

    return (
        <div className="app-container">
            <div className="header">
                <h1 className="title">ProfilerX</h1>
                <p className="subtitle">Discover and connect with people in your city</p>
            </div>
            <div className="form-container">
                <h2 className="h2">Add a New Profile</h2>
                <input
                    className="input-bg"
                    value={myForm.photoUrl ?? ""}
                    onChange={e => setMyForm({ ...myForm, photoUrl: e.target.value })}
                    placeholder="Photo url!"
                />
                <input
                    className="input-bg"
                    value={myForm.firstname}
                    onChange={e => setMyForm({ ...myForm, firstname: e.target.value })}
                    placeholder="Name!"
                />
                <input
                    className="input-bg"
                    value={myForm.lastname}
                    onChange={e => setMyForm({ ...myForm, lastname: e.target.value })}
                    placeholder="Last name!"
                />
                <input
                    className="input-bg"
                    value={myForm.age}
                    onChange={e => setMyForm({ ...myForm, age: Number.parseInt(e.target.value) })}
                    type="number"
                    placeholder="Age"
                />
                <input
                    className="input-bg"
                    value={myForm.occupation}
                    onChange={e => setMyForm({ ...myForm, occupation: e.target.value })}
                    placeholder="Occupation!"
                />
                <input
                    className="input-bg"
                    value={myForm.city}
                    onChange={e => setMyForm({ ...myForm, city: e.target.value })}
                    placeholder="City!"
                />
                <button className="create-btn" onClick={() => {
                    profilerClient.createProfile(myForm).then(result => {
                        setProfiles([...profiles, result])
                    })
                }}>
                    Create new profile
                </button>
            </div>
            <hr />
            <h2 className="h2">Profiles</h2>
            <div className="profile-list">
                {profiles.map(p => (
                    <div key={p.id} className="profile-card">
                        {p.photourl
                            ? <img src={p.photourl} alt="Profile" className="profile-img" />
                            : null}
                        <h3 className="profile-name">{p.firstname} {p.lastname}</h3>
                        <p>Age: {p.age}</p>
                        <p>Occupation: {p.occupation}</p>
                        <p>City: {p.city}</p>
                    </div>
                ))}
            </div>
        </div>
    )
}

export default App