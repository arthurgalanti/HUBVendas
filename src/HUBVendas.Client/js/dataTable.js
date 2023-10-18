async function getData() {
    const record = await fetch('http://localhost/api/v1/product');
    const data = records.json();

    let tab='';
    data.products.forEach(function(product){
        tab += `<tr>
            <td>${product.}</td>
        </tr>`
    })
}