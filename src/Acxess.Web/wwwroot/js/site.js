// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('alpine:init', () => {
    Alpine.data('layout', () => ({
        sidebarOpen: false, // Controla el sidebar en Móvil
        darkMode: localStorage.getItem('theme') === 'dark',

        init() {
            // Inicializar tema
            if (this.darkMode) document.documentElement.classList.add('dark');
            else document.documentElement.classList.remove('dark');
        },

        toggleSidebar() {
            this.sidebarOpen = !this.sidebarOpen;
        },

        toggleTheme() {
            this.darkMode = !this.darkMode;
            localStorage.setItem('theme', this.darkMode ? 'dark' : 'light');
            if (this.darkMode) document.documentElement.classList.add('dark');
            else document.documentElement.classList.remove('dark');
        }
    }));
    // "success", "error", "info"
    Alpine.store('notifications', {
        items: [],
        add(type, message) {
            const id = Date.now();
            this.items.push({ id, type, message });
            setTimeout(() => {
                this.remove(id);
            }, 4000);
        },
        remove(id) {
            this.items = this.items.filter(item => item.id !== id);
        }
    });
});

document.body.addEventListener('notify', (event) => {
    Alpine.store('notifications').add(event.detail.type, event.detail.message);
});