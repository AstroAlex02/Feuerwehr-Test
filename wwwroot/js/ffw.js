// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Dark Mode Toggle with localStorage

//document.addEventListener('DOMContentLoaded', function () {
//    const themeToggleBtn = document.getElementById('theme-toggle');
//    const themeToggleDarkIcon = document.getElementById('theme-toggle-dark-icon');
//    const themeToggleLightIcon = document.getElementById('theme-toggle-light-icon');

//    // Get current theme from localStorage or default to 'light'
//    const currentTheme = localStorage.getItem('theme') || 'light';

//    // Show the appropriate icon based on current theme
//    if (currentTheme === 'dark') {
//        themeToggleLightIcon.classList.remove('hidden');
//        document.documentElement.classList.add('dark');
//    } else {
//        themeToggleDarkIcon.classList.remove('hidden');
//        document.documentElement.classList.remove('dark');
//    }

//    // Toggle theme when button is clicked
//    themeToggleBtn.addEventListener('click', function () {
//        // Toggle icons
//        themeToggleDarkIcon.classList.toggle('hidden');
//        themeToggleLightIcon.classList.toggle('hidden');

//        // Toggle dark mode
//        if (document.documentElement.classList.contains('dark')) {
//            document.documentElement.classList.remove('dark');
//            localStorage.setItem('theme', 'light');
//        } else {
//            document.documentElement.classList.add('dark');
//            localStorage.setItem('theme', 'dark');
//        }
//    });
//});
