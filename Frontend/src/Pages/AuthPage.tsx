import { useState } from "react";
import "./AuthPage.css";

export default function AuthPage() {
  const [isSignup, setIsSignup] = useState(false);

  return (
    <div className={`auth-page ${isSignup ? "signup-mode" : ""}`}>
      <div className="auth-box">
        <div className="liquid-bg">
          <div className="liq-blob lb1"></div>
          <div className="liq-blob lb2"></div>
          <div className="liq-blob lb3"></div>
          <div className="liq-blob lb4"></div>
        </div>

        <div className="mobile-nav">
          <button
            type="button"
            className={`m-tab m-tab-si ${!isSignup ? "active" : ""}`}
            onClick={() => setIsSignup(false)}
          >
            Sign In
          </button>
          <button
            type="button"
            className={`m-tab m-tab-su ${isSignup ? "active" : ""}`}
            onClick={() => setIsSignup(true)}
          >
            Sign Up
          </button>
        </div>

        <div className={`panel pA ${isSignup ? "show-reg" : ""}`}>
          <div className="p-inner pA-login">
            <div className="fp-title with-logo">
              <img src="/logo.jpg" alt="MCZ Logo" className="auth-logo" />
              <span>SIGN IN</span>
            </div>

            <div className="social-row">
              <button type="button" className="s-icon" aria-label="Notification sign in">
                <svg viewBox="0 0 24 24" fill="none" stroke="#6878c0" strokeWidth="1.8" strokeLinecap="round">
                  <path d="M18 8A6 6 0 0 0 6 8c0 7-3 9-3 9h18s-3-2-3-9" />
                  <path d="M13.73 21a2 2 0 0 1-3.46 0" />
                </svg>
              </button>

              <button type="button" className="s-icon" aria-label="Chat sign in">
                <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="#6878c0"
                  strokeWidth="1.8"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                >
                  <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z" />
                </svg>
              </button>

              <button type="button" className="s-icon active" aria-label="Email sign in">
                <svg viewBox="0 0 24 24" fill="none" stroke="#6878c0" strokeWidth="1.8" strokeLinecap="round">
                  <rect x="2" y="6" width="20" height="12" rx="4" />
                  <path d="M8 12h4M10 10v4M15 12h.01M17 12h.01" />
                </svg>
              </button>
            </div>

            <div className="social-hint">Choose a login method or sign in with email</div>

            <div className="field">
              <input type="text" placeholder="Username / Email" autoComplete="off" />
            </div>

            <div className="field">
              <input type="password" placeholder="Password" />
            </div>

            <button type="button" className="forgot-link">
              Forgot password?
            </button>

            <button className="btn-blue btn-full" type="button">
              Sign In
            </button>
          </div>

          <div className="p-inner pA-reg">
            <div className="fp-title with-logo">
              <img src="/logo.jpg" alt="MCZ Logo" className="auth-logo" />
              <span>CREATE ACCOUNT</span>
            </div>

            <div className="social-row">
              <button type="button" className="s-icon" aria-label="Notification sign up">
                <svg viewBox="0 0 24 24" fill="none" stroke="#6878c0" strokeWidth="1.8" strokeLinecap="round">
                  <path d="M18 8A6 6 0 0 0 6 8c0 7-3 9-3 9h18s-3-2-3-9" />
                  <path d="M13.73 21a2 2 0 0 1-3.46 0" />
                </svg>
              </button>

              <button type="button" className="s-icon" aria-label="Chat sign up">
                <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="#6878c0"
                  strokeWidth="1.8"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                >
                  <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z" />
                </svg>
              </button>

              <button type="button" className="s-icon" aria-label="Email sign up">
                <svg viewBox="0 0 24 24" fill="none" stroke="#6878c0" strokeWidth="1.8" strokeLinecap="round">
                  <rect x="2" y="6" width="20" height="12" rx="4" />
                  <path d="M8 12h4M10 10v4M15 12h.01M17 12h.01" />
                </svg>
              </button>
            </div>

            <div className="social-hint">Choose a registration method or sign up with email</div>

            <div className="field">
              <input type="text" placeholder="Username" autoComplete="off" />
            </div>

            <div className="field">
              <input type="email" placeholder="Email" />
            </div>

            <div className="field">
              <input type="password" placeholder="Password" />
            </div>

            <button className="btn-blue btn-full" type="button">
              Sign Up
            </button>
          </div>
        </div>

        <div className={`panel pB ${isSignup ? "show-welcome" : ""}`}>
          <div className="d1"></div>
          <div className="d2"></div>

          <div className="p-inner pB-hello">
            <div className="o-title">Hello Friend!</div>
            <div className="o-sub">
              Create your account and become part of the Methodist Statistical Data Management platform.
            </div>
            <button className="btn-blue" type="button" onClick={() => setIsSignup(true)}>
              Sign Up
            </button>
          </div>

          <div className="p-inner pB-welcome">
            <div className="o-title">Welcome Back!</div>
            <div className="o-sub">
              Already have an account? Sign in and continue capturing and reviewing your circuit data.
            </div>
            <button className="btn-blue" type="button" onClick={() => setIsSignup(false)}>
              Sign In
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}