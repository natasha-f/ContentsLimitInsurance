function makeApiCall(url, newItem) {
    const esc = encodeURIComponent;
    const query = Object.keys(newItem)
        .map(k => `${esc(k)}=${esc(newItem[k])}`)
        .join("&");
    return fetch(url + "?" + query)
        .then((res) => res.json());
}

function Contents(props) {
    const nameRef = React.useRef();
    const priceRef = React.useRef();
    const [categoryId, setCategoryId] = React.useState(props.model.categories[0].id);
    const [added, setAdded] = React.useState(null);
    const [deleted, setDeleted] = React.useState(null);
    const [usedCategories, setUsedCategories] = React.useState(props.model.usedCategories);
    const [total, setTotal] = React.useState(props.model.total);

    const apiCall = (params, url) => {
        if (params) {
            makeApiCall(url, params)
                .then((result) => {
                    setUsedCategories(result.model.usedCategories);
                    setTotal(result.model.total);
                })
                .catch((e) => {
                    console.warn(e.message);
                });
        }
    }

    React.useEffect(() => {
        apiCall(added, props.addItemUrl);
        }, [added]);

    React.useEffect(() => {
        if (deleted) {
            apiCall({ id: deleted }, props.deleteItemUrl);
        }
    }, [deleted]);

    const handleAdd = e => {
        e.preventDefault();

        const name = nameRef.current.value;
        const price = priceRef.current.value;

        setAdded({ name, price, categoryId });
        nameRef.current.value = "";
        priceRef.current.value = 0;
    }

    return (
        <div className="border border-secondary bg-light d-flex justify-content-center height-75">
            <div className="border border-secondary mt-5 mb-5">
                <div className="ml-3">
                    
                        {usedCategories.map(({ categoryId, categoryName, categoryTotal, items }) => (
                            <div key={categoryId}>
                                <div className="row">
                                    <div className="col-8">{categoryName}</div>
                                    <div className="col-4">{categoryTotal}</div>
                                </div>
                                    
                                {items.map(({ id, name, price }) => (
                                    <div key={id} className="row ml-3">
                                        <div className="col-8">{name}</div>
                                        <div className="col-4">
                                            {price} <i className="fas fa-trash-alt" onClick={e => setDeleted(id)}></i>
                                        </div>
                                    </div>
                                ))}
                          </div>
                        ))}
                </div>

                <div className="m-3">
                    <div className="row">
                        <div className="col-8">TOTAL:</div>
                        <div className="col-4">{total}</div>
                    </div>
                </div>

                <div className="form-inline">
                    <input type="text" placeholder="Item Name" ref={nameRef} className="form-control"/>

                    <div className="input-group">
                        <div className="input-group-prepend">
                            <div className="input-group-text">$</div>
                        </div>
                        <input type="number" className="form-control" ref={priceRef}/>
                    </div>
                    <select onChange={e => setCategoryId(e.currentTarget.value)} className="form-control">
                        {props.model.categories.map(({ id, name }) => (
                            <option key={id} value={id}>{name}</option>
                        ))}
                    </select>
                    <button className="btn btn-primary" onClick={handleAdd}>Add</button>
                </div>
            </div>
        </div>
    );
}
