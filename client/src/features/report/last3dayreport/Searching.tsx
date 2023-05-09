import { dir } from "console";
import { fi } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import moment from "moment";
import { Button, Checkbox, Divider, Header, Icon, Label, Menu, Pagination, Portal, Search, Segment, Table, TransitionablePortal } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../mainreport/style.css";

export default observer(function Searching() {
    const { last3dayStore } = useStore();
    const { handleSearch} = last3dayStore;
    return (
        <Search onSearchChange={(e,data) => handleSearch(e,data)} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})