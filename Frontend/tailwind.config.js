/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      fontFamily: {
        arabic: ["Cairo", "normal"],
      },
      container: {
        center: true,
        padding: "1rem",
        screens: {
          sm: "640px",
          md: "750px",
          lg: "970px",
          xl: "1200px",
        },
      },
    },
  },
  plugins: [],
};
