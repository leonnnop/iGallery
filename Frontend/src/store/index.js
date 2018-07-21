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
        addAssign(state, currentAssign) {
            let detailAssign = {};
            detailAssign.taskId = currentAssign.jobid;
            detailAssign.taskState = currentAssign.taskState;
            detailAssign.sawId = currentAssign.id;
            detailAssign.sawTaskId = currentAssign.sawTaskId;
            detailAssign.machine = currentAssign.equipid;
            // detailAssign.machine = detailAssign.machine.join(',');
            detailAssign.workType = currentAssign.type;
            detailAssign.inner = currentAssign.inner;
            detailAssign.outer = currentAssign.outer;
            detailAssign.inBox = currentAssign.feedingbox;
            // detailAssign.inBox = detailAssign.inBox.join(',');
            detailAssign.outBox = currentAssign.finishedbox;
            // detailAssign.outBox = detailAssign.outBox.join(',');
            detailAssign.thick = currentAssign.thickness;
            detailAssign.name = currentAssign.name;
            detailAssign.draftid = currentAssign.draftid;
            detailAssign.mainShaftSpeed = currentAssign.mainShaftSpeed;
            detailAssign.feedSpeed = currentAssign.feedSpeed;
            detailAssign.param = currentAssign.param;

            state.detailAssigns.push(detailAssign);
            console.log(state.detailAssigns);
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