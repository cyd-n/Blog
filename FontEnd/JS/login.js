 function loginPage() {
    return {
      today: '',
      showPassword: false,
      loading: false,
      errorMsg: '',
      success: false,
      form: {
        email: '',
        password: '',
        remember: false
      },

      init() {
        this.today = new Date().toLocaleDateString('en-GB', {
          weekday: 'long', day: 'numeric', month: 'long', year: 'numeric'
        });
      },

      async handleLogin() {
        
        this.errorMsg = '';

        if (!this.form.email || !this.form.password) { this.errorMsg = '⚠ All fields are required, dear correspondent.'; return; }

        this.loading = true;

        try { // dont work
          const response = await fetch('http://localhost:5000/api/Auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
              email: this.form.email,
              password: this.form.password
            })
          });

          if (!response.ok) {
            const data = await response.json().catch(() => ({}));
            throw new Error(data.message || 'Invalid credentials. The gatekeepers remain unconvinced.');
          }

          const data = await response.json();

          if (data.token) {
            localStorage.setItem('token', data.token);
          }

          this.success = true;
          setTimeout(() => { window.location.href = 'index.html'; }, 1800);

        } catch (err) {
          this.errorMsg = '⚠ ' + err.message;
        } finally {
          this.loading = false;
        }
      }
    }
  }