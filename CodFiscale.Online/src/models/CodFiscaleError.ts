export class CodFiscaleError {
    constructor(errorMessage = "", errorSeverity: string|ErrorSeverity|null = null) {
        this.errorMessage = errorMessage;
        this.errorSeverity = errorSeverity;
    }
    get ErrorMessage() {
        return this.errorMessage;
    }

    set ErrorMessage(errorMessage) {
        this.errorMessage = errorMessage;
    }

    get ErrorSeverity() {
        return this.errorSeverity;
    }

    set ErrorSeverity(errorSeverity: string|ErrorSeverity|null) {
        this.errorSeverity = errorSeverity;
    }

    private errorMessage: string;
    private errorSeverity: string|ErrorSeverity|null;
}

enum ErrorSeverity {
    Info = "info",
    Warning = "warning",
    Danger = "danger",
    Success = "success"
}