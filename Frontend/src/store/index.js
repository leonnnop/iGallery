import Vue from 'vue'
import Vuex from 'vuex'
import {
    stat
} from 'fs';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        currentUserId: '277284652@qq.com',
        currentUserId_ID: '16',
        currentUsername: 'leonnnop',
        currentUserPassword:'liangchen_',
        currentUserBio: '233',
        currentUserPhoto: '',
        job: {
            taskId: '',
            createTime: null,
            taskState: -1,
            sawTaskList: [],
        },


        detailAssigns: [],
        detailAssign: {
            taskState: '',
            sawTaskId: 0,
            sawId: 0,
            workType: '',
            thick: '',
            inner: '',
            outer: '',
            inBox: [],
            outBox: [],
            machine: [],
            mainShaftSpeed: '',
            feedSpeed: '',
            name: '',
            draftid: '',
            param: '',
            number: '',
            costTime: '',
            taskId: '', //????????/
        },
    },
    mutations: {
        addCurrentUserId(state, currentId) {
            state.currentUserId = currentId;
        },
        addCurrentUserId_ID(state, currentId) {
            state.currentUserId_ID = currentId;
        },
        addCurrentUsername(state, username) {
            state.currentUsername = username;
        },
        addCurrentUserPhoto(state, photo) {
            state.currentUserPhoto = photo;
        },
        addCurrentUserPassword(state, password) {
            state.currentUserPassword = password;
        },
        addCurrentUserBio(state, bio) {
            state.currentUserBio = bio;
        },
        deleteAssign(state, index) {
            state.detailAssigns.splice(index, 1);
        },
        deleteAllAssign(state) {
            console.log('deleteAllAssign');
            state.detailAssigns.splice(0, state.detailAssigns.length);
        },
        beforeJobCommit(state, taskId) {
            // console.log('beforeJobCommit');
            // console.log('in beforeJobCommit:' + taskId);
            state.job.taskId = taskId,
                state.job.taskState = 0,
                // console.log('in beforeJobCommit:' + state.detailAssigns);/////??????????????
                state.job.sawTaskList = state.detailAssigns;
        }
    }
})