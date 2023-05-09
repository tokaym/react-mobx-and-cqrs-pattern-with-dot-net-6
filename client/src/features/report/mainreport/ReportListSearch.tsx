import { observer } from "mobx-react-lite";
import { Search } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";

export default observer(function ReportListSearch() {
    const { reportStore } = useStore();
    const { handleSearch, search} = reportStore;
    return (
        <Search onSearchChange={(e,data) => handleSearch(e,data)} value={search} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})