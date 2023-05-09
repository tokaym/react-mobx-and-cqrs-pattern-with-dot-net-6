import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { Button, Checkbox, Divider, Header, Icon, Label, Menu, Pagination, Portal, Search, Segment, Table, TransitionablePortal } from "semantic-ui-react";
import { number } from "yup/lib/locale";
import { useStore } from "../../../app/stores/store";
import "../../materialgroup/style.css";
import ReportListPaging from "./MaterialGroupListPaging";
import ReportListTable from "./MaterialGroupListTable";

export default observer(function MaterialGroupListSearch() {
    const { materialgroupStore } = useStore();
    const { handleSearch, search} = materialgroupStore;
    return (
        <Search onSearchChange={(e,data) => handleSearch(e,data)} value={search} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})