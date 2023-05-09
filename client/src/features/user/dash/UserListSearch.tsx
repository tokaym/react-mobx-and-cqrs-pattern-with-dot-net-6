import { observer } from "mobx-react-lite";
import { Search } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import "../../user/dash/userstyle.css";

export default observer(function UserListSearch() {
    const { userStore } = useStore();
    const { handleSearch, search} = userStore;
    return (
        <Search onSearchChange={(e,data) => handleSearch(e,data)} value={search} input={{ icon: 'search', iconPosition: 'left' }} />
    )
})