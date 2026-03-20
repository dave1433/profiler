import { useState } from "react";
import { generateGeminiContent } from "../services/geminiApi";

const DEFAULT_MODEL = "gemini-2.0-flash";

export function useGemini() {
    const [apiKey, setApiKey] = useState("");
    const [model, setModel] = useState(DEFAULT_MODEL);
    const [prompt, setPrompt] = useState("");
    const [responseText, setResponseText] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState("");

    const submitPrompt = async () => {
        if (!apiKey.trim()) {
            setError("Please provide your Gemini API key.");
            return;
        }

        if (!prompt.trim()) {
            setError("Please write a prompt first.");
            return;
        }

        setIsLoading(true);
        setError("");

        try {
            const result = await generateGeminiContent({
                apiKey: apiKey.trim(),
                prompt: prompt.trim(),
                model: model.trim() || DEFAULT_MODEL
            });
            setResponseText(result);
        } catch (e) {
            const message = e instanceof Error ? e.message : "Gemini request failed.";
            setError(message);
        } finally {
            setIsLoading(false);
        }
    };

    return {
        apiKey,
        setApiKey,
        model,
        setModel,
        prompt,
        setPrompt,
        responseText,
        isLoading,
        error,
        submitPrompt
    };
}

