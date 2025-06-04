/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      fontFamily: {
        arabic: ["Cairo", "sans-serif"],
      },
      borderRadius: {
        "4xl": "15rem",
      },
      container: {
        center: true,
        padding: "1.5rem",
        screens: {
          sm: "640px",
          md: "750px",
          lg: "970px",
          xl: "1200px",
          "2xl": "1300px",
        },
      },
      colors: {
        mainBg: {
          100: "#f6f5f3", //main white background... white
          200: "#F9FAFB",
          300: "#E7F1EE",
        },
        primary: {
          txt100: "#0B4632", //light green text
          txt200: "#06291D", //dark green text                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      third:
          table: "#B6D4CA",

          //green
          btn: "#127453", //button background color... green
          txt: "#127453", // green text exist in button...green
          bg: "#127453", // text numeric data color.... mintgreen

          light: "#ECFDF3",

          hov: "#DBEAE5",
          hovbtn: "#0B4632",
        },
        secondary: {
          //blue
          btn: "#1E88E5", // doctor accept btn by A.Health..blue
          table: "#2473AB", //vaccine table body background.... blue
          chart: "#60D3BC", //chart color
        },
        error: "#B4231B", //general vaccine table header background... red
        errorHov: "#7A271A",
        third: {
          //gray
          txt: "#8E98A8",
          border: "#E5E7EB",
          bg: "#DBEAE5",
        },
      },
    },
  },
  plugins: [],
};
