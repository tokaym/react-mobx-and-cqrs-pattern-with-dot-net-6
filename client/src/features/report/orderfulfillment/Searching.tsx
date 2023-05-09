import { observer } from "mobx-react-lite";
import { Search } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";

export default observer(function Searching() {
    const { fulfillmentStore } = useStore();
    const { handleSearch} = fulfillmentStore;
    return (
        <Search style={{ float:"right", marginBottom:"10px" }} onSearchChange={(e,data) => handleSearch(e,data)} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})