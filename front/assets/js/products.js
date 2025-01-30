
// Fetch products from server
function getCustomers() {
    const url = "https://localhost:7071/api/products/GetAll";
    return fetch(url)
      .then((res) => {
        if (!res.ok) {
          throw new Error(`HTTP Error: ${res.status}`);
        }
        return res.json();
      })
      .catch((err) => {
        console.log("Failed to fetch customers", err);
        return [];
      });
  }