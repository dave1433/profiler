interface GeminiSectionProps {
    apiKey: string;
    model: string;
    prompt: string;
    responseText: string;
    isLoading: boolean;
    error: string;
    onApiKeyChange: (value: string) => void;
    onModelChange: (value: string) => void;
    onPromptChange: (value: string) => void;
    onSubmit: () => void;
}

export function GeminiSection({
    apiKey,
    model,
    prompt,
    responseText,
    isLoading,
    error,
    onApiKeyChange,
    onModelChange,
    onPromptChange,
    onSubmit
}: GeminiSectionProps) {
    return (
        <section className="panel">
            <h2 className="h2">Gemini Assistant</h2>
            <p className="section-subtitle">Use your API key to generate content directly from the app.</p>

            <div className="gemini-form">
                <input
                    className="input"
                    type="password"
                    value={apiKey}
                    onChange={e => onApiKeyChange(e.target.value)}
                    placeholder="Gemini API key"
                />
                <input
                    className="input"
                    value={model}
                    onChange={e => onModelChange(e.target.value)}
                    placeholder="Model (example: gemini-2.0-flash)"
                />
                <textarea
                    className="text-area"
                    value={prompt}
                    onChange={e => onPromptChange(e.target.value)}
                    placeholder="Write your prompt"
                    rows={6}
                />
                <button className="submit-btn" onClick={onSubmit} disabled={isLoading}>
                    {isLoading ? "Generating..." : "Ask Gemini"}
                </button>
            </div>

            {error && <p className="error-text">{error}</p>}

            {responseText && (
                <div className="response-card">
                    <h3>Response</h3>
                    <p>{responseText}</p>
                </div>
            )}
        </section>
    );
}

