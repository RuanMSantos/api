const enviar = () => {
  document
    .getElementById("formulario")
    .addEventListener("submit", async (e) => {
      e.preventDefault();

      const formData = new FormData(e.target);
      const data = Object.fromEntries(formData.entries());

      const response = await fetch("http://localhost:5172/funcionario", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
      });

      const result = await response.json();
      alert(JSON.stringify(result, null, 2));
    });
};

enviar();
