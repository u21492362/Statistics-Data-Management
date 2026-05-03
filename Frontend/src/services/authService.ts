const API_BASE = "https://localhost:5001/api";

export type LoginRequest = {
  email: string;
  password: string;
};

export type RegisterRequest = {
  fullName: string;
  email: string;
  password: string;
  circuitId?: number | null;
  societyId?: number | null;
};

export type AuthResponse = {
  token: string;
  expiresAtUtc: string;
  userId: number;
  fullName: string;
  email: string;
  role: string;
  circuitId?: number | null;
  societyId?: number | null;
};

export const authService = {
  async login(payload: LoginRequest): Promise<AuthResponse> {
    const response = await fetch(`${API_BASE}/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    });

    if (!response.ok) {
      throw new Error("Invalid email or password.");
    }

    const data: AuthResponse = await response.json();

    localStorage.setItem("authToken", data.token);
    localStorage.setItem("currentUser", JSON.stringify(data));

    return data;
  },

  async register(payload: RegisterRequest): Promise<AuthResponse> {
    const response = await fetch(`${API_BASE}/auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Registration failed.");
    }

    const data: AuthResponse = await response.json();

    localStorage.setItem("authToken", data.token);
    localStorage.setItem("currentUser", JSON.stringify(data));

    return data;
  },

  logout() {
    localStorage.removeItem("authToken");
    localStorage.removeItem("currentUser");
  },

  getToken() {
    return localStorage.getItem("authToken");
  },

  getCurrentUser(): AuthResponse | null {
    const user = localStorage.getItem("currentUser");
    return user ? JSON.parse(user) : null;
  },

  isAuthenticated() {
    return !!localStorage.getItem("authToken");
  }
};